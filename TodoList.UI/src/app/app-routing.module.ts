import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Pages } from './core/enums/pages';

const routes: Routes = [
	{
		path: '',
		redirectTo: 'home',
		pathMatch: 'full'
	},
	{
		path: Pages.home,
		loadChildren: () =>
			import('./pages/home-page/home-page.module').then(m => m.HomePageModule)
	},
	{
		path: Pages.lists,
		loadChildren: () => 
			import('./pages/lists-page/lists-page.module').then(m => m.ListsPageModule)
	}
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule {}
