import { Component } from '@angular/core';
import { Observable, shareReplay } from 'rxjs';
import { TodoListSummaryDto } from 'src/app/core/http/clients/clients';
import { TodoListService } from 'src/app/core/http/todo-list.service';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent {
	todoLists$: Observable<TodoListSummaryDto[]>;

	constructor(todoListService: TodoListService) {
		this.todoLists$ = todoListService.Get().pipe(shareReplay(1));
	}
}
