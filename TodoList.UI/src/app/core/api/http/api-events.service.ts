import { Injectable } from '@angular/core';
import { Observable, Subject, filter } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class ApiEventsService {
	private publisher$ = new Subject<EventType>();

	constructor() {}

	public publish(event: EventType): void {
		this.publisher$.next(event);
	}

	public getSingle(eventType: EventType): Observable<EventType> {
		return this.getMany([eventType]);
	}

	public getMany(eventTypes: EventType[]): Observable<EventType> {
		return this.publisher$
			.asObservable()
			.pipe(filter(event => eventTypes.includes(event)));
	}
}

export enum EventType {
	TodoCreated,
	TodoRenamed,
	TodoCompleted,
	TodoListCreated,
	TodoListRenamed
}
