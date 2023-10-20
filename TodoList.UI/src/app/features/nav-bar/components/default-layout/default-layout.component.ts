import { Component, Input, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-default-layout',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss']
})
export class DefaultLayoutComponent {
	@Input() templateRef!: TemplateRef<unknown>;
}
