import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Pages } from 'src/app/core/enums/pages';

@Injectable({ providedIn: 'root' })
export class PageSelectorService {
	public currentPage!: Pages;
	public todoListId?: number;

	private pageMap = new Map<string, Pages>([
		['home', Pages.home],
		['lists', Pages.lists],
		['todo-list', Pages.list]
	]);

	constructor(private readonly router: Router) {
		let page = this.getPage();
		this.currentPage = page;
	}

	public select(page: Pages, id?: number): void {
		this.currentPage = page;
		this.todoListId = id ?? undefined;

		let route: string = page;

		if (!!id) route += `/${id}`;

		this.router.navigate([Pages.home]);
		this.router.navigate([route]);
	}

	private getPage(): Pages {
		let urlSegments = document.URL.split('/');
		let page: Pages | undefined = undefined;

		for (let segment of urlSegments) {
			if (this.pageMap.has(segment)) {
				page = this.pageMap.get(segment);
				break;
			}
		}
		if (page === Pages.list)
			this.todoListId = Number.parseInt(urlSegments[urlSegments.length -1]);

		console.log(this.todoListId);
		return page ?? Pages.home;
	}
}
