import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodoListComponent } from './components/todo-list/todo-list.component';
import { CoreModule } from 'src/app/core/core.module';
import { ReactiveFormsModule } from '@angular/forms';
import { TodoCardComponent } from './components/todo-card/todo-card.component';

@NgModule({
	declarations: [TodoListComponent, TodoCardComponent],
	imports: [CommonModule, CoreModule, ReactiveFormsModule],
	exports: [TodoListComponent]
})
export class TodoListModule {}
