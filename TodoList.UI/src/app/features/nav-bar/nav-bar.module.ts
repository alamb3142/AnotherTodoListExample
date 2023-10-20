import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DefaultLayoutComponent } from './components/default-layout/default-layout.component';

@NgModule({
	declarations: [SideNavComponent, DefaultLayoutComponent],
	imports: [CommonModule, FontAwesomeModule],
	exports: [SideNavComponent, DefaultLayoutComponent],
})
export class NavBarModule { }
