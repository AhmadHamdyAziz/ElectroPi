export interface TicketListItem {
  id: string;
  title: string;
  status: TicketState;
  priority: TicketPriority;
  customerId: string;
  assignedAgentId: string | null;
  createdAt: string;
}

export interface TicketDetails {
  id: string;
  title: string;
  description: string;
  status: TicketState;
  priority: TicketPriority;
  customerId: string;
  assignedAgentId: string | null;
  createdAt: string;
}

export enum TicketState {
  Open = 0,
  InProgress = 1,
  Resolved = 2,
  Closed = 3
}

export enum TicketPriority {
  Low = 0,
  Medium = 1,
  High = 2,
  Critical = 3
}

export interface CreateTicketRequest {
  title: string;
  description: string;
  priority: TicketPriority;
}

export interface TicketComment {
  id: string;
  comment: string;
  authorId: string;
  createdAt: string;
}

export interface AddTicketCommentRequest {
  comment: string;
}