import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketActions } from './ticket-actions';

describe('TicketActions', () => {
  let component: TicketActions;
  let fixture: ComponentFixture<TicketActions>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketActions],
    }).compileComponents();

    fixture = TestBed.createComponent(TicketActions);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
