import { DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

import {
  TicketDetails as Ticket,
  TicketPriority,
  TicketState
} from '../../../shared/models/Ticket.models';

import { TicketService } from '../../../shared/services/TicketService/ticket.service';

import { TicketCommentsComponent } from './ticket-comments/ticket-comments';
import { TicketActivityComponent } from './ticket-activity/ticket-activity';
import { TicketActionsComponent } from './ticket-actions/ticket-actions';

import { MatDivider } from "@angular/material/divider";

@Component({
  selector: 'app-ticket-details-new',
  imports: [
    DatePipe,
    MatCardModule,
    MatIconModule,
    RouterLink,
    TicketCommentsComponent,
    TicketActivityComponent,
    TicketActionsComponent,
    MatDivider
],
  templateUrl: './ticket-details-new.html',
  styleUrl: './ticket-details-new.scss',
})
export class TicketDetailsNew {

  private readonly route = inject(ActivatedRoute);
  private readonly ticketService = inject(TicketService);

  readonly ticket = signal<Ticket | null>(null);

 private readonly ticketId = this.route.snapshot.paramMap.get('id');

constructor() {
  this.loadTicket();
}

private loadTicket(): void {
  const id = this.ticketId;

  if (!id) {
    return;
  }

  this.ticketService.getTicket(id).subscribe({
    next: ticket => {
      this.ticket.set(ticket);
    },
    error: error => {
      console.error('Failed to load ticket', error);
    }
  });
}

reloadTicket(): void {
  this.loadTicket();
}

  getStatusLabel(status: number): string {
    return TicketState[status] ?? 'Unknown';
  }

  getPriorityLabel(priority: number): string {
    return TicketPriority[priority] ?? 'Unknown';
  }

  getStatusClass(status: number): string {
    return `status-${TicketState[status]?.toLowerCase() ?? 'unknown'}`;
  }

  getPriorityClass(priority: number): string {
    return `priority-${TicketPriority[priority]?.toLowerCase() ?? 'unknown'}`;
  }
}