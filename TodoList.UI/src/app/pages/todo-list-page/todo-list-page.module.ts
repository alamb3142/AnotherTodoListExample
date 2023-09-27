import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TodoListPageRoutingModule } from './todo-list-page-routing.module';
import { TodoListPageComponent } from './todo-list-page.component';


@NgModule({
  declarations: [
    TodoListPageComponent
  ],
  imports: [
    CommonModule,
    TodoListPageRoutingModule
  ]
})
export class TodoListPageModule { }
