import { Injectable } from '@angular/core';
import {
	GetAllTodoListsQuery,
	TodoListClient,
	TodoListSummaryDto
} from './clients/clients';
import { Observable, Subject, map, shareReplay, startWith, switchMap, tap } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class TodoListService {
	public todoLists$!: Observable<TodoListSummaryDto[]>;

	private refreshSource$ = new Subject<void>();
	private refresh$!: Observable<void>;

	constructor(private readonly client: TodoListClient) {
		this.refresh$ = this.refreshSource$
			.asObservable()
			.pipe(startWith(undefined));

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
			tap(() => this.refreshSource$.next())
		);
	}

	public Rename(todoListId: number, newName: string): Observable<void> {
		return this.client
			.rename(todoListId, newName)
			.pipe(tap(() => this.refreshSource$.next()));
	}
}
