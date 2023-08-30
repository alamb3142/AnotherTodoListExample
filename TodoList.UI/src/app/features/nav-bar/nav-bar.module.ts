import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DefaultLayoutComponent } from './components/default-layout/default-layout.component';
import { PageSelectorService } from './services/page-selector.service';

@NgModule({
	declarations: [SideNavComponent, DefaultLayoutComponent],
	imports: [CommonModule, FontAwesomeModule],
	exports: [SideNavComponent, DefaultLayoutComponent],
	providers: [PageSelectorService]
})
export class NavBarModule { }
