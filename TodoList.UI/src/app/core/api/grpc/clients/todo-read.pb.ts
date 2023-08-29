/* tslint:disable */
/* eslint-disable */
// @ts-nocheck
//
// THIS IS A GENERATED FILE
// DO NOT MODIFY IT! YOUR CHANGES WILL BE LOST
import {
  GrpcMessage,
  RecursivePartial,
  ToProtobufJSONOptions
} from '@ngx-grpc/common';
import { BinaryReader, BinaryWriter, ByteSource } from 'google-protobuf';

/**
 * Message implementation for todo_read.TodoDto
 */
export class TodoDto implements GrpcMessage {
  static id = 'todo_read.TodoDto';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new TodoDto();
    TodoDto.deserializeBinaryFromReader(instance, new BinaryReader(bytes));
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: TodoDto) {
    _instance.id = _instance.id || 0;
    _instance.description = _instance.description || '';
    _instance.completed = _instance.completed || false;
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: TodoDto,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          _instance.id = _reader.readInt32();
          break;
        case 2:
          _instance.description = _reader.readString();
          break;
        case 3:
          _instance.completed = _reader.readBool();
          break;
        default:
          _reader.skipField();
      }
    }

    TodoDto.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(_instance: TodoDto, _writer: BinaryWriter) {
    if (_instance.id) {
      _writer.writeInt32(1, _instance.id);
    }
    if (_instance.description) {
      _writer.writeString(2, _instance.description);
    }
    if (_instance.completed) {
      _writer.writeBool(3, _instance.completed);
    }
  }

  private _id: number;
  private _description: string;
  private _completed: boolean;

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of TodoDto to deeply clone from
   */
  constructor(_value?: RecursivePartial<TodoDto.AsObject>) {
    _value = _value || {};
    this.id = _value.id;
    this.description = _value.description;
    this.completed = _value.completed;
    TodoDto.refineValues(this);
  }
  get id(): number {
    return this._id;
  }
  set id(value: number) {
    this._id = value;
  }
  get description(): string {
    return this._description;
  }
  set description(value: string) {
    this._description = value;
  }
  get completed(): boolean {
    return this._completed;
  }
  set completed(value: boolean) {
    this._completed = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    TodoDto.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): TodoDto.AsObject {
    return {
      id: this.id,
      description: this.description,
      completed: this.completed
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): TodoDto.AsProtobufJSON {
    return {
      id: this.id,
      description: this.description,
      completed: this.completed
    };
  }
}
export module TodoDto {
  /**
   * Standard JavaScript object representation for TodoDto
   */
  export interface AsObject {
    id: number;
    description: string;
    completed: boolean;
  }

  /**
   * Protobuf JSON representation for TodoDto
   */
  export interface AsProtobufJSON {
    id: number;
    description: string;
    completed: boolean;
  }
}

/**
 * Message implementation for todo_read.GetAllTodosRequest
 */
export class GetAllTodosRequest implements GrpcMessage {
  static id = 'todo_read.GetAllTodosRequest';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new GetAllTodosRequest();
    GetAllTodosRequest.deserializeBinaryFromReader(
      instance,
      new BinaryReader(bytes)
    );
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: GetAllTodosRequest) {}

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: GetAllTodosRequest,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        default:
          _reader.skipField();
      }
    }

    GetAllTodosRequest.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: GetAllTodosRequest,
    _writer: BinaryWriter
  ) {}

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of GetAllTodosRequest to deeply clone from
   */
  constructor(_value?: RecursivePartial<GetAllTodosRequest.AsObject>) {
    _value = _value || {};
    GetAllTodosRequest.refineValues(this);
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    GetAllTodosRequest.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): GetAllTodosRequest.AsObject {
    return {};
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): GetAllTodosRequest.AsProtobufJSON {
    return {};
  }
}
export module GetAllTodosRequest {
  /**
   * Standard JavaScript object representation for GetAllTodosRequest
   */
  export interface AsObject {}

  /**
   * Protobuf JSON representation for GetAllTodosRequest
   */
  export interface AsProtobufJSON {}
}

/**
 * Message implementation for todo_read.GetAllTodosResponse
 */
export class GetAllTodosResponse implements GrpcMessage {
  static id = 'todo_read.GetAllTodosResponse';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new GetAllTodosResponse();
    GetAllTodosResponse.deserializeBinaryFromReader(
      instance,
      new BinaryReader(bytes)
    );
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: GetAllTodosResponse) {
    _instance.todos = _instance.todos || [];
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: GetAllTodosResponse,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          const messageInitializer1 = new TodoDto();
          _reader.readMessage(
            messageInitializer1,
            TodoDto.deserializeBinaryFromReader
          );
          (_instance.todos = _instance.todos || []).push(messageInitializer1);
          break;
        default:
          _reader.skipField();
      }
    }

    GetAllTodosResponse.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: GetAllTodosResponse,
    _writer: BinaryWriter
  ) {
    if (_instance.todos && _instance.todos.length) {
      _writer.writeRepeatedMessage(
        1,
        _instance.todos as any,
        TodoDto.serializeBinaryToWriter
      );
    }
  }

  private _todos?: TodoDto[];

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of GetAllTodosResponse to deeply clone from
   */
  constructor(_value?: RecursivePartial<GetAllTodosResponse.AsObject>) {
    _value = _value || {};
    this.todos = (_value.todos || []).map(m => new TodoDto(m));
    GetAllTodosResponse.refineValues(this);
  }
  get todos(): TodoDto[] | undefined {
    return this._todos;
  }
  set todos(value: TodoDto[] | undefined) {
    this._todos = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    GetAllTodosResponse.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): GetAllTodosResponse.AsObject {
    return {
      todos: (this.todos || []).map(m => m.toObject())
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): GetAllTodosResponse.AsProtobufJSON {
    return {
      todos: (this.todos || []).map(m => m.toProtobufJSON(options))
    };
  }
}
export module GetAllTodosResponse {
  /**
   * Standard JavaScript object representation for GetAllTodosResponse
   */
  export interface AsObject {
    todos?: TodoDto.AsObject[];
  }

  /**
   * Protobuf JSON representation for GetAllTodosResponse
   */
  export interface AsProtobufJSON {
    todos: TodoDto.AsProtobufJSON[] | null;
  }
}

/**
 * Message implementation for todo_read.GetFilteredTodosRequest
 */
export class GetFilteredTodosRequest implements GrpcMessage {
  static id = 'todo_read.GetFilteredTodosRequest';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new GetFilteredTodosRequest();
    GetFilteredTodosRequest.deserializeBinaryFromReader(
      instance,
      new BinaryReader(bytes)
    );
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: GetFilteredTodosRequest) {
    _instance.searchTerm = _instance.searchTerm || '';
    _instance.offset = _instance.offset || 0;
    _instance.fetchNum = _instance.fetchNum || 0;
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: GetFilteredTodosRequest,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          _instance.searchTerm = _reader.readString();
          break;
        case 2:
          _instance.offset = _reader.readInt32();
          break;
        case 3:
          _instance.fetchNum = _reader.readInt32();
          break;
        default:
          _reader.skipField();
      }
    }

    GetFilteredTodosRequest.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: GetFilteredTodosRequest,
    _writer: BinaryWriter
  ) {
    if (_instance.searchTerm) {
      _writer.writeString(1, _instance.searchTerm);
    }
    if (_instance.offset) {
      _writer.writeInt32(2, _instance.offset);
    }
    if (_instance.fetchNum) {
      _writer.writeInt32(3, _instance.fetchNum);
    }
  }

  private _searchTerm: string;
  private _offset: number;
  private _fetchNum: number;

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of GetFilteredTodosRequest to deeply clone from
   */
  constructor(_value?: RecursivePartial<GetFilteredTodosRequest.AsObject>) {
    _value = _value || {};
    this.searchTerm = _value.searchTerm;
    this.offset = _value.offset;
    this.fetchNum = _value.fetchNum;
    GetFilteredTodosRequest.refineValues(this);
  }
  get searchTerm(): string {
    return this._searchTerm;
  }
  set searchTerm(value: string) {
    this._searchTerm = value;
  }
  get offset(): number {
    return this._offset;
  }
  set offset(value: number) {
    this._offset = value;
  }
  get fetchNum(): number {
    return this._fetchNum;
  }
  set fetchNum(value: number) {
    this._fetchNum = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    GetFilteredTodosRequest.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): GetFilteredTodosRequest.AsObject {
    return {
      searchTerm: this.searchTerm,
      offset: this.offset,
      fetchNum: this.fetchNum
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): GetFilteredTodosRequest.AsProtobufJSON {
    return {
      searchTerm: this.searchTerm,
      offset: this.offset,
      fetchNum: this.fetchNum
    };
  }
}
export module GetFilteredTodosRequest {
  /**
   * Standard JavaScript object representation for GetFilteredTodosRequest
   */
  export interface AsObject {
    searchTerm: string;
    offset: number;
    fetchNum: number;
  }

  /**
   * Protobuf JSON representation for GetFilteredTodosRequest
   */
  export interface AsProtobufJSON {
    searchTerm: string;
    offset: number;
    fetchNum: number;
  }
}

/**
 * Message implementation for todo_read.GetFilteredTodosResponse
 */
export class GetFilteredTodosResponse implements GrpcMessage {
  static id = 'todo_read.GetFilteredTodosResponse';

  /**
   * Deserialize binary data to message
   * @param instance message instance
   */
  static deserializeBinary(bytes: ByteSource) {
    const instance = new GetFilteredTodosResponse();
    GetFilteredTodosResponse.deserializeBinaryFromReader(
      instance,
      new BinaryReader(bytes)
    );
    return instance;
  }

  /**
   * Check all the properties and set default protobuf values if necessary
   * @param _instance message instance
   */
  static refineValues(_instance: GetFilteredTodosResponse) {
    _instance.todos = _instance.todos || [];
  }

  /**
   * Deserializes / reads binary message into message instance using provided binary reader
   * @param _instance message instance
   * @param _reader binary reader instance
   */
  static deserializeBinaryFromReader(
    _instance: GetFilteredTodosResponse,
    _reader: BinaryReader
  ) {
    while (_reader.nextField()) {
      if (_reader.isEndGroup()) break;

      switch (_reader.getFieldNumber()) {
        case 1:
          const messageInitializer1 = new TodoDto();
          _reader.readMessage(
            messageInitializer1,
            TodoDto.deserializeBinaryFromReader
          );
          (_instance.todos = _instance.todos || []).push(messageInitializer1);
          break;
        default:
          _reader.skipField();
      }
    }

    GetFilteredTodosResponse.refineValues(_instance);
  }

  /**
   * Serializes a message to binary format using provided binary reader
   * @param _instance message instance
   * @param _writer binary writer instance
   */
  static serializeBinaryToWriter(
    _instance: GetFilteredTodosResponse,
    _writer: BinaryWriter
  ) {
    if (_instance.todos && _instance.todos.length) {
      _writer.writeRepeatedMessage(
        1,
        _instance.todos as any,
        TodoDto.serializeBinaryToWriter
      );
    }
  }

  private _todos?: TodoDto[];

  /**
   * Message constructor. Initializes the properties and applies default Protobuf values if necessary
   * @param _value initial values object or instance of GetFilteredTodosResponse to deeply clone from
   */
  constructor(_value?: RecursivePartial<GetFilteredTodosResponse.AsObject>) {
    _value = _value || {};
    this.todos = (_value.todos || []).map(m => new TodoDto(m));
    GetFilteredTodosResponse.refineValues(this);
  }
  get todos(): TodoDto[] | undefined {
    return this._todos;
  }
  set todos(value: TodoDto[] | undefined) {
    this._todos = value;
  }

  /**
   * Serialize message to binary data
   * @param instance message instance
   */
  serializeBinary() {
    const writer = new BinaryWriter();
    GetFilteredTodosResponse.serializeBinaryToWriter(this, writer);
    return writer.getResultBuffer();
  }

  /**
   * Cast message to standard JavaScript object (all non-primitive values are deeply cloned)
   */
  toObject(): GetFilteredTodosResponse.AsObject {
    return {
      todos: (this.todos || []).map(m => m.toObject())
    };
  }

  /**
   * Convenience method to support JSON.stringify(message), replicates the structure of toObject()
   */
  toJSON() {
    return this.toObject();
  }

  /**
   * Cast message to JSON using protobuf JSON notation: https://developers.google.com/protocol-buffers/docs/proto3#json
   * Attention: output differs from toObject() e.g. enums are represented as names and not as numbers, Timestamp is an ISO Date string format etc.
   * If the message itself or some of descendant messages is google.protobuf.Any, you MUST provide a message pool as options. If not, the messagePool is not required
   */
  toProtobufJSON(
    // @ts-ignore
    options?: ToProtobufJSONOptions
  ): GetFilteredTodosResponse.AsProtobufJSON {
    return {
      todos: (this.todos || []).map(m => m.toProtobufJSON(options))
    };
  }
}
export module GetFilteredTodosResponse {
  /**
   * Standard JavaScript object representation for GetFilteredTodosResponse
   */
  export interface AsObject {
    todos?: TodoDto.AsObject[];
  }

  /**
   * Protobuf JSON representation for GetFilteredTodosResponse
   */
  export interface AsProtobufJSON {
    todos: TodoDto.AsProtobufJSON[] | null;
  }
}
