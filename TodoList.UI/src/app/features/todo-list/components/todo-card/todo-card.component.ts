import {
	Component,
	EventEmitter,
	Input,
	OnDestroy,
	Output
} from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { TodoDto } from 'src/app/core/api/http/clients/clients';
import { TodoService } from 'src/app/core/api/http/todo.service';

@Component({
	selector: 'app-todo-card',
	templateUrl: './todo-card.component.html',
	styleUrls: ['./todo-card.component.scss']
})
export class TodoCardComponent implements OnDestroy {
	@Input() public todo!: TodoDto;
	@Output() public todoCompleted = new EventEmitter<void>();

	private destroy$ = new Subject<void>();

	constructor(private readonly todoService: TodoService) {}

	public complete(): void {
		if (this.todo.completed) return;

		this.todoService
			.complete(this.todo.id)
			.pipe(takeUntil(this.destroy$))
			.subscribe();
	}

	public ngOnDestroy(): void {
		this.destroy$.complete();
	}
}
