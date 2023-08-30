import { Injectable, signal } from '@angular/core';
import {
	GetAllTodoListsQuery,
	TodoListClient,
	TodoListSummaryDto
} from './clients/clients';
import { Observable, Subject, map, startWith, switchMap, tap } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class TodoListService {
	private refreshSource$ = new Subject<void>();
	private refresh$!: Observable<void>;

	constructor(private readonly client: TodoListClient) {
		this.refresh$ = this.refreshSource$
			.asObservable()
			.pipe(startWith(undefined));
	}

	public Create(name: string): Observable<void> {
		return this.client.todoListsPost(name).pipe(map(() => undefined));
	}

	public Get(): Observable<TodoListSummaryDto[]> {
		return this.refresh$.pipe(
			switchMap(() =>
				this.client.todoListsGet({} as GetAllTodoListsQuery)
			)
		);
	}

	public Rename(todoListId: number, newName: string): Observable<void> {
		return this.client
			.rename(todoListId, newName)
			.pipe(tap(() => this.refreshSource$.next()));
	}
}
