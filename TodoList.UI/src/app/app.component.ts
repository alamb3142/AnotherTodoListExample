import { Component } from '@angular/core';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent {
	title = 'TodoList.UI';

	constructor() {
		// TODO: theme support, currently just hard coding dark mode
		document.documentElement.className = "theme-dark2"
	}
}
