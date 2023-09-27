import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodoListPageComponent } from './todo-list-page.component';

const routes: Routes = [
	{ path: ':id', component: TodoListPageComponent, pathMatch: 'full' }
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class TodoListPageRoutingModule {}
