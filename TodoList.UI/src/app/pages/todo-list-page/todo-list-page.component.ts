import { Component, Inject, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-todo-list-page',
  templateUrl: './todo-list-page.component.html',
  styleUrls: ['./todo-list-page.component.scss']
})
export class TodoListPageComponent {
	public todoListId!: number;

	constructor(route: ActivatedRoute) {
		this.todoListId = route.snapshot.params['id'];
	}
}
