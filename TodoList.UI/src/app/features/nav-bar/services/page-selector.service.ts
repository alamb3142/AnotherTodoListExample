import { Injectable } from '@angular/core';

@Injectable({
	providedIn: 'root'
})
export class PageSelectorService {
	public currentPage: string;

	constructor() {
		this.currentPage = this.getUrl(); 
	}

	private getUrl(): string {
		let urlSegments = document.URL.split('/');
		let route = urlSegments[urlSegments.length - 1];
		return route.split('?')[0];
	}
}
