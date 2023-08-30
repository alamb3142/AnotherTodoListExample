import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListOverviewComponent } from './components/list-overview/list-overview.component';

@NgModule({
	declarations: [ListOverviewComponent],
	imports: [CommonModule],
	exports: [ListOverviewComponent]
})
export class ListOverviewModule { }
