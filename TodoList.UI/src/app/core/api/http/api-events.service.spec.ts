import { fakeAsync, tick } from '@angular/core/testing';
import { ApiEventsService, EventType } from './api-events.service';

describe('ApiEventsService', () => {
	let service: ApiEventsService;
	jasmine.DEFAULT_TIMEOUT_INTERVAL = 100;

	beforeEach(() => {
		service = new ApiEventsService();
	});

	it('publishes events', fakeAsync(() => {
		let emitted = false;
		let eventType = {} as EventType;
		service.getMany([EventType.TodoCreated]).subscribe(event => {
			eventType = event;
			emitted = true;
		});

		service.publish(EventType.TodoCreated);
		tick(100);

		expect(emitted)
			.withContext('publish TodoCreated emits to subscriber')
			.toBeTrue();
		expect(eventType).toBe(EventType.TodoCreated);
	}));

	it('does not emit when event type not subscribed to', fakeAsync(() => {
		let emitted;
		service
			.getMany([
				EventType.TodoCreated,
				EventType.TodoListCreated,
				EventType.TodoCompleted
			])
			.subscribe(() => (emitted = true));

		service.publish(EventType.TodoRenamed);
		tick(200);

		expect(emitted).toBeUndefined();
	}));
});
