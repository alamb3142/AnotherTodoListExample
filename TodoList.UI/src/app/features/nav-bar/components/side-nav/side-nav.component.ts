import { Component } from '@angular/core';
import { Observable, shareReplay } from 'rxjs';
import {
	faHouse,
	faCalendar,
	faPlus,
	faList
} from '@fortawesome/free-solid-svg-icons';
import { PageSelectorService } from '../../services/page-selector.service';
import { Pages } from 'src/app/core/enums/pages';
import { TodoListService } from 'src/app/core/api/http/todo-list.service';
import { TodoListSummaryDto } from 'src/app/core/api/http/clients/clients';

@Component({
	selector: 'app-side-nav',
	templateUrl: './side-nav.component.html',
	styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent {
	public todoLists$: Observable<TodoListSummaryDto[]>;
	public icons = ICONS;
	public pages = Pages;

	constructor(
		todoListService: TodoListService,
		private readonly pageSelector: PageSelectorService
	) {
		this.todoLists$ = todoListService.todoLists$.pipe(shareReplay(1));
	}

	public isSelected(page: Pages, todoListId?: number): boolean {
		const pageSelected = page === this.pageSelector.currentPage;
		const todoListIdSelected = todoListId
			? todoListId === this.pageSelector.todoListId
			: true;
		return pageSelected && todoListIdSelected;
	}

	public home(): void {
		this.pageSelector.select(Pages.home);
	}

	public lists(): void {
		this.pageSelector.select(Pages.lists);
	}

	public list(id: number): void {
		this.pageSelector.select(Pages.list, id);
	}
}

const ICONS = {
	home: faHouse,
	calendar: faCalendar,
	list: faList,
	plus: faPlus
};
