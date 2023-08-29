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
import * as thisProto from './todo-read.pb';
import { GRPC_TODO_QUERY_CLIENT_SETTINGS } from './todo-read.pbconf';
/**
 * Service client implementation for todo_read.TodoQuery
 */
@Injectable({ providedIn: 'any' })
export class TodoQueryClient {
  private client: GrpcClient<any>;

  /**
   * Raw RPC implementation for each service client method.
   * The raw methods provide more control on the incoming data and events. E.g. they can be useful to read status `OK` metadata.
   * Attention: these methods do not throw errors when non-zero status codes are received.
   */
  $raw = {
    /**
     * Unary call: /todo_read.TodoQuery/GetAll
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.GetAllTodosResponse>>
     */
    getAll: (
      requestData: thisProto.GetAllTodosRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.GetAllTodosResponse>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/todo_read.TodoQuery/GetAll',
        requestData,
        requestMetadata,
        requestClass: thisProto.GetAllTodosRequest,
        responseClass: thisProto.GetAllTodosResponse
      });
    },
    /**
     * Unary call: /todo_read.TodoQuery/GetFiltered
     *
     * @param requestMessage Request message
     * @param requestMetadata Request metadata
     * @returns Observable<GrpcEvent<thisProto.GetFilteredTodosResponse>>
     */
    getFiltered: (
      requestData: thisProto.GetFilteredTodosRequest,
      requestMetadata = new GrpcMetadata()
    ): Observable<GrpcEvent<thisProto.GetFilteredTodosResponse>> => {
      return this.handler.handle({
        type: GrpcCallType.unary,
        client: this.client,
        path: '/todo_read.TodoQuery/GetFiltered',
        requestData,
        requestMetadata,
        requestClass: thisProto.GetFilteredTodosRequest,
        responseClass: thisProto.GetFilteredTodosResponse
      });
    }
  };

  constructor(
    @Optional() @Inject(GRPC_TODO_QUERY_CLIENT_SETTINGS) settings: any,
    @Inject(GRPC_CLIENT_FACTORY) clientFactory: GrpcClientFactory<any>,
    private handler: GrpcHandler
  ) {
    this.client = clientFactory.createClient('todo_read.TodoQuery', settings);
  }

  /**
   * Unary call @/todo_read.TodoQuery/GetAll
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.GetAllTodosResponse>
   */
  getAll(
    requestData: thisProto.GetAllTodosRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.GetAllTodosResponse> {
    return this.$raw
      .getAll(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }

  /**
   * Unary call @/todo_read.TodoQuery/GetFiltered
   *
   * @param requestMessage Request message
   * @param requestMetadata Request metadata
   * @returns Observable<thisProto.GetFilteredTodosResponse>
   */
  getFiltered(
    requestData: thisProto.GetFilteredTodosRequest,
    requestMetadata = new GrpcMetadata()
  ): Observable<thisProto.GetFilteredTodosResponse> {
    return this.$raw
      .getFiltered(requestData, requestMetadata)
      .pipe(throwStatusErrors(), takeMessages());
  }
}
