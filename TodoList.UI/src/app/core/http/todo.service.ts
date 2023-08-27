import { Injectable } from '@angular/core';
import { TodoClient, TodoDto } from './clients/clients';
import { Observable, map } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class TodoService {
	constructor(private readonly client: TodoClient) { }

	public create(title: string): Observable<void> {
		return this.client.todosPost(title).pipe(map(() => undefined));
	}

	public complete(todoId: number): Observable<void> {
		return this.client.complete(todoId);
	}

	public rename(todoId: number, newName: string): Observable<void> {
		return this.client.rename(todoId, newName);
	}

	public addToList(todoId: number, todoListId: number): Observable<void> {
		return this.client.addToList(todoListId, todoId);
	}

	public getAll(): Observable<TodoDto[]> {
		return this.client.todosGet().pipe(map(x => x.todos ?? []));
	}

	public getForList(todoListId: number): Observable<TodoDto[]> {
		return this.client.forList(todoListId);
	}

	public getFiltered(
		searchTerm?: string,
		offset?: number,
		fetchNum?: number
	): Observable<TodoDto[]> {
		return this.client
			.filtered(searchTerm, offset, fetchNum)
			.pipe(map(x => x.todos ?? []));
	}
}
