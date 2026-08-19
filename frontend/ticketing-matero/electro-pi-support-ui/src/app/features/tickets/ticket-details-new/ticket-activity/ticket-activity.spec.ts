import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketActivity } from './ticket-activity';

describe('TicketActivity', () => {
  let component: TicketActivity;
  let fixture: ComponentFixture<TicketActivity>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketActivity],
    }).compileComponents();

    fixture = TestBed.createComponent(TicketActivity);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
