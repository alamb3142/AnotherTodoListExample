import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormControl, NonNullableFormBuilder } from '@angular/forms';
import {
	Observable,
	Subject,
	combineLatest,
	map,
	switchMap,
	takeUntil,
	tap
} from 'rxjs';
import { TodoDto } from 'src/app/core/api/http/clients/clients';
import { TodoListService } from 'src/app/core/api/http/todo-list.service';
import { TodoService } from 'src/app/core/api/http/todo.service';

@Component({
	selector: 'app-todo-list',
	templateUrl: './todo-list.component.html',
	styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit, OnDestroy {
	@Input() public todoListId?: Observable<number>;
	@Input() public title?: string;

	public newTodo: FormControl<string> = this.formBuilder.control('');
	public todos$!: Observable<TodoDto[]>;

	private currentTodoListId?: number = undefined;
	private destroy$ = new Subject<void>();

	constructor(
		private readonly todoService: TodoService,
		private readonly todoListService: TodoListService,
		private readonly formBuilder: NonNullableFormBuilder
	) {}

	public ngOnInit(): void {
		const todos$: Observable<TodoDto[]> = this.todoListId
			? this.filterByTodoList(this.todoListId)
			: this.todoService.todos$;

		this.todos$ = todos$.pipe(
			map(todos => todos.filter(t => !t.completed))
		);

		if (!this.title && this.todoListId) {
			combineLatest([this.todoListId, this.todoListService.todoLists$])
				.pipe(takeUntil(this.destroy$))
				.subscribe(([todoListId, todoLists]) => {
					this.title = todoLists.find(
						list => list.id == todoListId
					)?.title;
				});
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

	private filterByTodoList(todoListId$: Observable<number>) {
		return todoListId$.pipe(
			tap(id => (this.currentTodoListId = id)),
			switchMap(id =>
				this.todoService.todos$.pipe(
					map(todos => todos.filter(t => t.todoListId == id))
				)
			)
		);
	}
}
