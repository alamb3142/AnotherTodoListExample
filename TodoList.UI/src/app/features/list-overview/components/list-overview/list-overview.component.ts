import { Component, OnDestroy } from '@angular/core';
import { FormControl, NonNullableFormBuilder } from '@angular/forms';
import { Observable, Subject, takeUntil } from 'rxjs';
import { TodoListSummaryDto } from 'src/app/core/api/http/clients/clients';
import { TodoListService } from 'src/app/core/api/http/todo-list.service';

@Component({
	selector: 'app-list-overview',
	templateUrl: './list-overview.component.html',
	styleUrls: ['./list-overview.component.scss']
})
export class ListOverviewComponent implements OnDestroy {
	public lists$: Observable<TodoListSummaryDto[]>;
	public newListControl: FormControl<string> = this.formBuilder.control('');

	private destroy$ = new Subject<void>();

	constructor(
		private readonly todoListService: TodoListService,
		private readonly formBuilder: NonNullableFormBuilder
	) {
		this.lists$ = this.todoListService.Get();
	}

	public addTodoList(): void {
		this.todoListService
			.Create(this.newListControl.value)
			.pipe(takeUntil(this.destroy$))
			.subscribe(() => this.newListControl.reset());
	}

	public handleKeyPress(event: KeyboardEvent): void {
		if (event.key == 'Enter') {
			this.addTodoList();
		}
	}

	public ngOnDestroy(): void {
		this.destroy$.complete();
	}
}
