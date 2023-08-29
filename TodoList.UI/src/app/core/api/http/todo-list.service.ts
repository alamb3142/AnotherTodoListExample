import { Injectable } from '@angular/core';
import {
	GetAllTodoListsQuery,
	TodoListClient,
	TodoListSummaryDto
} from './clients/clients';
import { Observable, map } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class TodoListService {
	constructor(private readonly client: TodoListClient) { }

	public Create(name: string): Observable<void> {
		return this.client.todoListsPost(name).pipe(map(() => undefined));
	}

	public Get(): Observable<TodoListSummaryDto[]> {
		return this.client.todoListsGet(new GetAllTodoListsQuery());
	}

	public Rename(todoListId: number, newName: string): Observable<void> {
		return this.client.rename(todoListId, newName);
	}
}
