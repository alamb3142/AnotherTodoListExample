import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomePageComponent } from './home-page.component';
import { RouterModule, Routes } from '@angular/router';
import { TodoListModule } from 'src/app/features/todo-list/todo-list.module';
import { CoreModule } from 'src/app/core/core.module';
import { NavBarModule } from 'src/app/features/nav-bar/nav-bar.module';

const routes: Routes = [
	{
		path: '',
		component: HomePageComponent
	}
];

@NgModule({
	declarations: [HomePageComponent],
	imports: [
		CommonModule,
		TodoListModule,
		RouterModule.forChild(routes),
		CoreModule,
		NavBarModule
	],
	exports: [CoreModule]
})
export class HomePageModule {}
