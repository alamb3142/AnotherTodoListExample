import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodoListPageRoutingModule } from './todo-list-page-routing.module';
import { TodoListPageComponent } from './todo-list-page.component';
import { NavBarModule } from 'src/app/features/nav-bar/nav-bar.module';
import { TodoListModule } from '../../features/todo-list/todo-list.module';

@NgModule({
	declarations: [TodoListPageComponent],
	imports: [
		CommonModule,
		TodoListPageRoutingModule,
		NavBarModule,
		TodoListModule
	]
})
export class TodoListPageModule { }
