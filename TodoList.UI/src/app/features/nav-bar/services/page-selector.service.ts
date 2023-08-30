import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Pages } from 'src/app/core/enums/pages';

@Injectable()
export class PageSelectorService {
	constructor(private readonly router: Router) {}

	public select(page: Pages): void {
		this.router.navigate([page]);
	}

	currentPage(): string {
		let urlSegments = document.URL.split('/');
		let route = urlSegments[urlSegments.length - 1];
		return route.split('?')[0];
	}
}
