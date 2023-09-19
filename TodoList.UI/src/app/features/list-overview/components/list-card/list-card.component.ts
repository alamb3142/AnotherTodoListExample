import { Component, Input } from '@angular/core';
import { TodoListSummaryDto } from 'src/app/core/api/http/clients/clients';

@Component({
  selector: 'app-list-card',
  templateUrl: './list-card.component.html',
  styleUrls: ['./list-card.component.scss']
})
export class ListCardComponent {
	@Input() todoList!: TodoListSummaryDto;
}
