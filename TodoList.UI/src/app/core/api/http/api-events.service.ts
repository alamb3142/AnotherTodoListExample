import { Injectable } from '@angular/core';
import { Observable, Subject, filter } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class ApiEventsService {
	private publisher$ = new Subject<EventType>();

	public publish(event: EventType): void {
		this.publisher$.next(event);
	}

	public subscriberFor(eventType: EventType): Observable<EventType> {
		return this.subscribersFor([eventType]);
	}

	public subscribersFor(eventTypes: EventType[]): Observable<EventType> {
		return this.publisher$
			.asObservable()
			.pipe(filter(event => eventTypes.includes(event)));
	}
}

export enum EventType {
	// Todos
	TodoCreated,
	TodoRenamed,
	TodoCompleted,
	TodoMovedToList,
	// TodoLists
	TodoListCreated,
	TodoListRenamed
}
