import { Injectable, OnDestroy } from '@angular/core';
import { TodoClient, TodoDto } from './clients/clients';
import {
	Observable,
	Subject,
	catchError,
	map,
	of,
	shareReplay,
	startWith,
	switchMap,
	takeUntil,
	tap
} from 'rxjs';
import { ApiEventsService, EventType } from './api-events.service';

@Injectable({
	providedIn: 'root'
})
export class TodoService implements OnDestroy {
	/**
	 * All the User's todos, emits whenever new changes are available
	 *
	 * @remarks
	 * Will only trigger http requests if subscribed to, and will not
	 * trigger additional http requests as subscribers are added
	 *
	 * @example
	 * ```html
	 * <div *ngFor="let todo of todos$ | async">
	 * 	<!-- Display the todos, list will update as required -->
	 * </div>
	 * ```
	 */
	public todos$!: Observable<TodoDto[]>;

	private refresh$!: Observable<void>;
	private destroy$ = new Subject<void>();

	constructor(
		private readonly client: TodoClient,
		private readonly eventBus: ApiEventsService
	) {
		this.refresh$ = this.subscribeTo([
			EventType.TodoCreated,
			EventType.TodoCompleted,
			EventType.TodoRenamed
		]);

		this.todos$ = this.refresh$.pipe(
			switchMap(() => this.client.todosGet()),
			map(result => result.todos ?? []),
			shareReplay(1)
		);
	}

	public create(title: string, todoListId?: number): Observable<void> {
		return this.client.todosPost(title, todoListId).pipe(
			catchError(() => of(undefined)), // TODO: fix create endpoint
			map(() => undefined),
			tap(() => this.eventBus.publish(EventType.TodoCreated))
		);
	}

	public complete(todoId: number): Observable<void> {
		return this.client
			.complete(todoId)
			.pipe(tap(() => this.eventBus.publish(EventType.TodoCompleted)));
	}

	public rename(todoId: number, newName: string): Observable<void> {
		return this.client
			.rename(todoId, newName)
			.pipe(tap(() => this.eventBus.publish(EventType.TodoRenamed)));
	}

	public addToList(todoId: number, todoListId: number): Observable<void> {
		return this.client.addToList(todoListId, todoId).pipe(
			takeUntil(this.destroy$),
			tap(() => this.eventBus.publish(EventType.TodoMovedToList))
		);
	}

	public ngOnDestroy(): void {
		this.destroy$.complete();
	}

	private subscribeTo(events: EventType[]): Observable<void> {
		return this.eventBus.subscribersFor(events).pipe(
			map(() => undefined),
			startWith(undefined),
			shareReplay(1)
		);
	}
}
