import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Pages } from 'src/app/core/enums/pages';

@Injectable()
export class PageSelectorService {
	constructor(private readonly router: Router) {}

	public select(page: Pages, id?: number): void {
		let route: string = page;

		if (!!id) route += `/${id}`;

		this.router.navigate([Pages.home]);
		this.router.navigate([route]);
	}

	currentPage(): string {
		let urlSegments = document.URL.split('/');
		let route = urlSegments[urlSegments.length - 1];
		return route.split('?')[0];
	}
}
