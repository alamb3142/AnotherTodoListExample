import { Component, Inject, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, map } from 'rxjs';

@Component({
  selector: 'app-todo-list-page',
  templateUrl: './todo-list-page.component.html',
  styleUrls: ['./todo-list-page.component.scss']
})
export class TodoListPageComponent {
	public todoListId!: Observable<number>;

	constructor(route: ActivatedRoute) {
		this.todoListId = route.params.pipe(map(params => params['id']))
	}
}
