import { Injectable } from '@angular/core';
import {
	GetAllTodoListsQuery,
	TodoListClient,
	TodoListSummaryDto
} from './clients/clients';
import { Observable, map, shareReplay, startWith, switchMap, tap } from 'rxjs';
import { ApiEventsService, EventType } from './api-events.service';

@Injectable({
	providedIn: 'root'
})
export class TodoListService {
	public todoLists$!: Observable<TodoListSummaryDto[]>;

	private refresh$!: Observable<void>;

	constructor(
		private readonly client: TodoListClient,
		private readonly eventBus: ApiEventsService
	) {
		this.refresh$ = this.subscribeTo([
			EventType.TodoListCreated,
			EventType.TodoListRenamed,
			EventType.TodoCreated,
			EventType.TodoMovedToList,
			EventType.TodoCompleted
		]);

		this.todoLists$ = this.refresh$.pipe(
			switchMap(() =>
				this.client.todoListsGet({} as GetAllTodoListsQuery)
			),
			shareReplay(1)
		);
	}

	public Create(name: string): Observable<void> {
		return this.client.todoListsPost(name).pipe(
			map(() => undefined),
			tap(() => this.eventBus.publish(EventType.TodoListCreated))
		);
	}

	public Rename(todoListId: number, newName: string): Observable<void> {
		return this.client
			.rename(todoListId, newName)
			.pipe(tap(() => this.eventBus.publish(EventType.TodoListRenamed)));
	}
	private subscribeTo(events: EventType[]): Observable<void> {
		return this.eventBus.subscribersFor(events).pipe(
			map(() => undefined),
			startWith(undefined),
			shareReplay(1)
		);
	}
}
