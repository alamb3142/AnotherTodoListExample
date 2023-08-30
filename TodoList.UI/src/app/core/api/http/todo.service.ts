import { Injectable } from '@angular/core';
import { TodoClient, TodoDto } from './clients/clients';
import {
	Observable,
	Subject,
	map,
	startWith,
	switchMap,
	tap
} from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class TodoService {
	private refreshSource$ = new Subject<void>();
	private refresh$!: Observable<void>;

	constructor(private readonly client: TodoClient) {
		this.refresh$ = this.refreshSource$
			.asObservable()
			.pipe(startWith(undefined));
	}

	public create(title: string): Observable<void> {
		return this.client.todosPost(title).pipe(
			map(() => undefined),
			tap(() => this.refreshSource$.next())
		);
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
		return this.client
			.addToList(todoListId, todoId)
			.pipe(tap(() => this.refreshSource$.next()));
	}

	public getAll(): Observable<TodoDto[]> {
		return this.refresh$.pipe(
			switchMap(() => this.client.todosGet()),
			map(result => result.todos ?? [])
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
			switchMap(() => {
				return this.client.filtered(searchTerm, offset, fetchNum);
			}),
			map(result => result.todos ?? [])
		);
	}
}
