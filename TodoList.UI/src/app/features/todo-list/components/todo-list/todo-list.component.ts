import { Component, Input, OnDestroy } from '@angular/core';
import { FormControl, NonNullableFormBuilder } from '@angular/forms';
import { Observable, Subject, takeUntil } from 'rxjs';
import { TodoDto } from 'src/app/core/api/http/clients/clients';
import { TodoService } from 'src/app/core/api/http/todo.service';

@Component({
	selector: 'app-todo-list',
	templateUrl: './todo-list.component.html',
	styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnDestroy {
	@Input() public todoListId?: number;

	public newTodo: FormControl<string> = this.formBuilder.control('');
	public todos$: Observable<TodoDto[]>;

	private destroy$ = new Subject<void>();

	constructor(
		private readonly todoService: TodoService,
		private readonly formBuilder: NonNullableFormBuilder
	) {
		this.todos$ = !!this.todoListId
			? this.todoService.getForList(this.todoListId)
			: this.todoService.getFiltered();
	}

	public addTodo(): void {
		this.todoService
			.create(this.newTodo.value)
			.pipe(takeUntil(this.destroy$))
			.subscribe(() => this.newTodo.reset());
	}

	public handleKeyPress(event: KeyboardEvent): void {
		if (event.key == 'Enter') {
			this.addTodo();
		}
	}

	public ngOnDestroy(): void {
		this.destroy$.complete();
	}
}
