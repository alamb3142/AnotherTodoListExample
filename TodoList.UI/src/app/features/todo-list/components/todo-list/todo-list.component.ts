import { Component, Input, OnDestroy } from '@angular/core';
import { FormControl, NonNullableFormBuilder } from '@angular/forms';
import { Observable, Subject, map, switchMap, takeUntil, tap } from 'rxjs';
import { TodoDto } from 'src/app/core/api/http/clients/clients';
import { TodoService } from 'src/app/core/api/http/todo.service';

@Component({
	selector: 'app-todo-list',
	templateUrl: './todo-list.component.html',
	styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnDestroy {
	@Input() public todoListId?: Observable<number>;
	@Input() public title!: string;

	public newTodo: FormControl<string> = this.formBuilder.control('');
	public todos$!: Observable<TodoDto[]>;

	private currentTodoListId?: number = undefined;
	private destroy$ = new Subject<void>();

	constructor(
		private readonly todoService: TodoService,
		private readonly formBuilder: NonNullableFormBuilder
	) { }

	public ngOnInit(): void {
		if (!!this.todoListId) {
			this.todos$ = this.todoListId.pipe(
				tap(id => (this.currentTodoListId = id)),
				switchMap(id =>
					this.todoService.todos$.pipe(
						map(todos => todos.filter(t => t.todoListId == id))
					)
				)
			);
		} else {
			this.todos$ = this.todoService.todos$;
		}
	}

	public addTodo(): void {
		this.todoService
			.create(this.newTodo.value, this.currentTodoListId)
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
