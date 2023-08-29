import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { GetFilteredTodosRequest, TodoDto } from './clients/todo-read.pb';
import { TodoQueryClient } from './clients/todo-read.pbsc';
import {
	CreateTodoRequest,
	DeleteTodosRequest,
	UpdateTodoRequest,
	UpdateTodoResponse
} from './clients/todo-write.pb';
import { TodoCommandClient } from './clients/todo-write.pbsc';

@Injectable({
	providedIn: 'root'
})
export class TodoDataService {
	constructor(
		private readonly readClient: TodoQueryClient,
		private readonly writeClient: TodoCommandClient
	) {}

	add(description: string): Observable<void> {
		const request = new CreateTodoRequest({
			description
		});
		return this.writeClient.create(request).pipe(map(() => undefined));
	}

	update(
		id: number,
		description: string,
		completed: boolean
	): Observable<UpdateTodoResponse> {
		const request = new UpdateTodoRequest({ id, description, completed });
		return this.writeClient.update(request);
	}

	get(searchTerm: string): Observable<TodoDto[]> {
		const request = new GetFilteredTodosRequest();
		request.searchTerm = searchTerm;

		return this.readClient.getFiltered(request).pipe(
			map(response => {
				return response.todos ?? [];
			})
		);
	}

	delete(id: number): Observable<void> {
		const request = new DeleteTodosRequest({ id });
		return this.writeClient.delete(request).pipe(map(() => undefined));
	}
}
