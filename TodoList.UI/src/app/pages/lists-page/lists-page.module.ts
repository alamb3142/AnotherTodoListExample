import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListsPageComponent } from './lists-page.component';
import { TodoListModule } from 'src/app/features/todo-list/todo-list.module';
import { RouterModule } from '@angular/router';
import { NavBarModule } from 'src/app/features/nav-bar/nav-bar.module';

@NgModule({
	declarations: [ListsPageComponent],
	imports: [
		CommonModule,
		TodoListModule,
		RouterModule.forChild([{ path: '', component: ListsPageComponent }]),
		NavBarModule
	]
})
export class ListsPageModule { }
