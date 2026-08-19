import {
  Component,
  input,
  output
} from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

import {
  TicketDetails as Ticket,
  TicketPriority,
  TicketState
} from '../../../../shared/models/Ticket.models';

@Component({
  selector: 'app-ticket-actions',
  imports: [
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule
  ],
  templateUrl: './ticket-actions.html',
  styleUrl: './ticket-actions.scss',
})
export class TicketActionsComponent {

  readonly ticket = input.required<Ticket>();

  readonly actionCompleted = output<void>();

  readonly states = Object
    .keys(TicketState)
    .filter(key => !Number.isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: TicketState[Number(key)]
    }));

  readonly priorities = Object
    .keys(TicketPriority)
    .filter(key => !Number.isNaN(Number(key)))
    .map(key => ({
      value: Number(key),
      label: TicketPriority[Number(key)]
    }));

  onActionCompleted(): void {
    this.actionCompleted.emit();
  }
}