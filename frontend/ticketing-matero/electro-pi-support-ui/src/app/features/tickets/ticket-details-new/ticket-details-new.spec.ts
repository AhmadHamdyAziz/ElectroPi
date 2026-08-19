import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketDetailsNew } from './ticket-details-new';

describe('TicketDetailsNew', () => {
  let component: TicketDetailsNew;
  let fixture: ComponentFixture<TicketDetailsNew>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketDetailsNew],
    }).compileComponents();

    fixture = TestBed.createComponent(TicketDetailsNew);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
