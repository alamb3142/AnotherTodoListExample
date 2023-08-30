import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListsPageComponent } from './lists-page.component';
import { RouterModule } from '@angular/router';
import { NavBarModule } from 'src/app/features/nav-bar/nav-bar.module';
import { ListOverviewModule } from 'src/app/features/list-overview/list-overview.module';

@NgModule({
	declarations: [ListsPageComponent],
	imports: [
		CommonModule,
		RouterModule.forChild([{ path: '', component: ListsPageComponent }]),
		NavBarModule,
		ListOverviewModule
	]
})
export class ListsPageModule { }
