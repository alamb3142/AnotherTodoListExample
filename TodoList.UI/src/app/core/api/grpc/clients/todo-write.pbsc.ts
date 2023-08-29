/* tslint:disable */
/* eslint-disable */
// @ts-nocheck
//
// THIS IS A GENERATED FILE
// DO NOT MODIFY IT! YOUR CHANGES WILL BE LOST
import { Inject, Injectable, Optional } from '@angular/core';
import {
  GrpcCallType,
  GrpcClient,
  GrpcClientFactory,
  GrpcEvent,
  GrpcMetadata
} from '@ngx-grpc/common';
import {
  GRPC_CLIENT_FACTORY,
  GrpcHandler,
  takeMessages,
  throwStatusErrors
} from '@ngx-grpc/core';
import { Observable } from 'rxjs';
import * as thisProto from './todo-write.pb';
import { GRPC_TODO_COMMAND_CLIENT_SETTINGS } from './todo-write.pbconf';
/**
 * Service client implementation for todo_write.TodoCommand
 */
@Injectable({ providedIn: 'any' })
export class TodoCommandClient {
  private client: GrpcClient<any>;

  /**
   * Raw RPC implementation for each service client method.
   * The raw methods provide more control on the incoming data and events. E.g. they can be useful to read status `OK` metadata.
   * Attention: these methods do not throw errors when non-zero status codes are received.
   */
  $raw = {
    /**
     * Unary call: /todo_write.TodoCommand/Create
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.CreateTodoResponse>>
     */
    create: (
      requestData: thisProto.CreateTodoRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.CreateTodoResponse>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/todo_write.TodoCommand/Create',
        requestData,
        requestMetadata,
        requestClass: thisProto.CreateTodoRequest,
        responseClass: thisProto.CreateTodoResponse
      });
    },
    /**
     * Unary call: /todo_write.TodoCommand/Update
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.UpdateTodoResponse>>
     */
    update: (
      requestData: thisProto.UpdateTodoRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.UpdateTodoResponse>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/todo_write.TodoCommand/Update',
        requestData,
        requestMetadata,
        requestClass: thisProto.UpdateTodoRequest,
        responseClass: thisProto.UpdateTodoResponse
      });
    },
    /**
     * Unary call: /todo_write.TodoCommand/Delete
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.DeleteTodosResponse>>
     */
    delete: (
      requestData: thisProto.DeleteTodosRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.DeleteTodosResponse>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/todo_write.TodoCommand/Delete',
        requestData,
        requestMetadata,
        requestClass: thisProto.DeleteTodosRequest,
        responseClass: thisProto.DeleteTodosResponse
      });
    }
  };

  constructor(
    @Optional() @Inject(GRPC_TODO_COMMAND_CLIENT_SETTINGS) settings: any,
    @Inject(GRPC_CLIENT_FACTORY) clientFactory: GrpcClientFactory<any>,
    private handler: GrpcHandler
  ) {
    this.client = clientFactory.createClient(
      'todo_write.TodoCommand',
      settings
    );
  }

  /**
   * Unary call @/todo_write.TodoCommand/Create
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.CreateTodoResponse>
   */
  create(
    requestData: thisProto.CreateTodoRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.CreateTodoResponse> {
    return this.$raw
      .create(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }

  /**
   * Unary call @/todo_write.TodoCommand/Update
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.UpdateTodoResponse>
   */
  update(
    requestData: thisProto.UpdateTodoRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.UpdateTodoResponse> {
    return this.$raw
      .update(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }

  /**
   * Unary call @/todo_write.TodoCommand/Delete
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.DeleteTodosResponse>
   */
  delete(
    requestData: thisProto.DeleteTodosRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.DeleteTodosResponse> {
    return this.$raw
      .delete(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }
}
