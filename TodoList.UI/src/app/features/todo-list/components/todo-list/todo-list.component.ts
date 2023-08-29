import { Component, Input } from '@angular/core';
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
import { TodoDto } from 'src/app/core/api/http/clients/clients';
import { TodoService } from 'src/app/core/api/http/todo.service';

@Component({
	selector: 'app-todo-list',
	templateUrl: './todo-list.component.html',
	styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent {
	@Input() public todoListId?: number;

	newTodo: FormControl<string> = this.fb.control('');
	// searchTodos: FormControl<string> = this.fb.control('');
	todos$: Observable<TodoDto[]>;

	private refreshRequested$ = new BehaviorSubject<void>(undefined);

	constructor(
		private readonly todoService: TodoService,
		private readonly fb: NonNullableFormBuilder
	) {
		// const searchTodos$ = this.searchTodos.valueChanges.pipe(
		// 	debounceTime(300),
		// 	startWith('')
		// );
		this.todos$ = this.refreshRequested$.pipe(
			startWith(undefined),
			switchMap(() => {
				return !!this.todoListId
					? this.todoService.getForList(this.todoListId)
					: this.todoService.getAll();
			})
		);
	}

	addTodo() {
		this.todoService
			.create(this.newTodo.value)
			.pipe(
				tap(() => {
					this.newTodo.reset();
					this.refreshRequested$.next();
				})
			)
			.subscribe();
	}

	completeTodo(todo: TodoDto) {
		todo.completed = !todo.completed;
		this.todoService.complete(todo.id!).subscribe(() => {
			this.refreshRequested$.next();
		});
	}

	handleKeyPress(event: KeyboardEvent) {
		if (event.key == 'Enter') {
			this.addTodo();
		}
	}
}
