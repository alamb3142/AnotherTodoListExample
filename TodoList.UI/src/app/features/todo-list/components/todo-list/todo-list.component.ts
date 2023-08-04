import { Component } from '@angular/core';
import { FormControl, NonNullableFormBuilder } from '@angular/forms';
import {
	BehaviorSubject,
	Observable,
	combineLatest,
	debounceTime,
	startWith,
	switchMap,
	tap
} from 'rxjs';
import { TodoDto } from 'src/app/core/api/clients/todo-read.pb';
import { TodoDataService } from 'src/app/core/api/todo-data.service';

@Component({
	selector: 'app-todo-list',
	templateUrl: './todo-list.component.html',
	styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent {
	newTodo: FormControl<string> = this.fb.control('');
	searchTodos: FormControl<string> = this.fb.control('');
	todos$: Observable<TodoDto[]>;

	private refreshRequested$ = new BehaviorSubject<void>(undefined);

	constructor(
		private readonly todoService: TodoDataService,
		private readonly fb: NonNullableFormBuilder
	) {
		const searchTodos$ = this.searchTodos.valueChanges.pipe(
			debounceTime(300),
			startWith('')
		);

		this.todos$ = combineLatest([
			searchTodos$,
			this.refreshRequested$.pipe(startWith(undefined))
		]).pipe(
			switchMap(([searchTerm]) => {
				return this.todoService.get(searchTerm);
			})
		);
	}

	addTodo() {
		this.todoService
			.add(this.newTodo.value)
			.pipe(
				tap(() => {
					this.newTodo.reset();
					this.refreshRequested$.next();
				})
			)
			.subscribe();
	}

	toggleTodo(todo: TodoDto) {
		todo.completed = !todo.completed;
		this.todoService
			.update(todo.id, todo.description, todo.completed)
			.subscribe(() => {
				this.refreshRequested$.next();
			});
	}

	deleteTodo(todo: TodoDto) {
		this.todoService
			.delete(todo.id)
			.pipe(
				tap(() => {
					this.refreshRequested$.next();
				})
			)
			.subscribe();
	}

	handleKeyPress(event: KeyboardEvent) {
		if (event.key == 'Enter') {
			this.addTodo();
		}
	}

	getTodoId(index: number, todo: TodoDto): number {
		return todo.id;
	}
}
