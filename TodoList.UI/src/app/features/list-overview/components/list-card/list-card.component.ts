import { Component, Input } from '@angular/core';
import { TodoListSummaryDto } from 'src/app/core/api/http/clients/clients';
import { Pages } from 'src/app/core/enums/pages';
import { PageSelectorService } from 'src/app/features/nav-bar/services/page-selector.service';

@Component({
  selector: 'app-list-card',
  templateUrl: './list-card.component.html',
  styleUrls: ['./list-card.component.scss']
})
export class ListCardComponent {
	@Input() todoList!: TodoListSummaryDto;

	constructor(private readonly pageSelector: PageSelectorService) {}

	public navigateToList(): void {
		this.pageSelector.select(Pages.list, this.todoList.id);
	}
}
