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

@Injectable({
	providedIn: 'root'
})
export class TodoService implements OnDestroy {
	public todos$!: Observable<TodoDto[]>;

	private refresh$!: Observable<void>;
	private refreshSource$ = new Subject<void>();
	private destroy$ = new Subject<void>();

	constructor(private readonly client: TodoClient) {
		// Create refresh$ here because .asObservable creates
		// a new Observable & we want to share a single instance
		// across the service
		this.refresh$ = this.refreshSource$
			.asObservable()
			.pipe(startWith(undefined));

		this.todos$ =  this.refresh$.pipe(
			switchMap(() => this.client.todosGet()),
			map(result => {
				let todos = result.todos ?? [];

				return todos.filter(t => t.completed == false);
			}),
			shareReplay(1)
		);
	}

	public create(title: string, todoListId?: number): Observable<void> {
		let response$ = this.client.todosPost(title, todoListId).pipe(
			catchError(() => of(undefined)), // TODO: fix create endpoint
			map(() => undefined)
		);

		return response$.pipe(tap(() => this.refreshSource$.next()));
	}

	public complete(todoId: number): Observable<void> {
		return this.client
			.complete(todoId)
			.pipe(tap(() => this.refreshSource$.next()));
	}

	public rename(todoId: number, newName: string): Observable<void> {
		return this.client
			.rename(todoId, newName)
			.pipe(tap(() => this.refreshSource$.next()));
	}

	public addToList(todoId: number, todoListId: number): Observable<void> {
		return this.client.addToList(todoListId, todoId).pipe(
			takeUntil(this.destroy$),
			tap(() => this.refreshSource$.next())
		);
	}

	public getForList(todoListId: number): Observable<TodoDto[]> {
		return this.refresh$.pipe(
			switchMap(() => this.client.forList(todoListId))
		);
	}

	public getFiltered(
		searchTerm?: string,
		offset?: number,
		fetchNum?: number
	): Observable<TodoDto[]> {
		return this.refresh$.pipe(
			switchMap(() => this.client.filtered(searchTerm, offset, fetchNum)),
			map(result => {
				let todos = result.todos ?? [];

				return todos.filter(t => t.completed == false);
			})
		);
	}

	public ngOnDestroy(): void {
		this.destroy$.complete();
	}
}
