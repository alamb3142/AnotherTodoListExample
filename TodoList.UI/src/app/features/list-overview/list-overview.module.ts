import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListOverviewComponent } from './components/list-overview/list-overview.component';
import { ListCardComponent } from './components/list-card/list-card.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
	declarations: [ListOverviewComponent, ListCardComponent],
	imports: [CommonModule, ReactiveFormsModule],
	exports: [ListOverviewComponent]
})
export class ListOverviewModule {}
