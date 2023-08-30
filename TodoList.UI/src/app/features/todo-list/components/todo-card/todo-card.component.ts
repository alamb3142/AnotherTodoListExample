import {
	Component,
	EventEmitter,
	Input,
	OnDestroy,
	OnInit,
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
export class TodoCardComponent implements OnInit, OnDestroy {
	@Input() public todo!: TodoDto;
	@Output() public todoCompleted = new EventEmitter<void>();
	public disabled: boolean = false;

	private destroy$ = new Subject<void>();

	constructor(private readonly todoService: TodoService) { }

	public ngOnInit(): void {
		this.disabled = this.todo.completed;
	}

	public complete(): void {
		if (this.disabled) return;

		this.disabled = true;
		this.todoService
			.complete(this.todo.id)
			.pipe(takeUntil(this.destroy$))
			.subscribe();
	}

	ngOnDestroy(): void {
		this.destroy$.complete();
	}
}
