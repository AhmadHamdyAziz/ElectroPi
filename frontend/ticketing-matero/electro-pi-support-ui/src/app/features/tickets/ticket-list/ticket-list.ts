import {
  ChangeDetectionStrategy,
  Component,
  OnInit,
  inject,
  signal
} from '@angular/core';

import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';

import { TicketService } from '../../../shared/services/TicketService/ticket.service';
import { TicketListItem, TicketPriority, TicketState } from '../../../shared/models/Ticket.models';

import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-ticket-list',
  imports: [
    MatButtonModule,
    MatCardModule,
    MatPaginatorModule,
    MatTableModule,
    MatIconModule,
    MatTooltipModule,
    RouterLink
  ],
  templateUrl: './ticket-list.html',
  styleUrl: './ticket-list.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TicketList implements OnInit {

  private readonly ticketService = inject(TicketService);

  readonly tickets = signal<TicketListItem[]>([]);
  readonly totalTickets = signal(0);
  readonly loading = signal(false);

  readonly displayedColumns = [
    'id',
    'title',
    'status',
    'priority',
    'createdAt',
    'actions'
  ];

  pageIndex = 0;
  pageSize = 10;

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {

    this.loading.set(true);

    this.ticketService
      .getTickets(
        this.pageIndex + 1,
        this.pageSize
      )
      .subscribe({
        next: result => {
          this.tickets.set(result.items);
          this.totalTickets.set(result.totalCount);
          this.loading.set(false);
        },

        error: error => {
          console.error('Failed to load tickets', error);
          this.loading.set(false);
        }
      });
  }

  onPageChange(event: PageEvent): void {

    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;

    this.loadTickets();
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