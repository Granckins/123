%%------------------------------------------------------------
%%
%% Implementation stub file
%% 
%% Target: CosTime_TimeService
%% Source: /net/isildur/ldisk/daily_build/r16b02_prebuild_opu_o.2013-09-16_20/otp_src_R16B02/lib/cosTime/src/CosTime.idl
%% IC vsn: 4.3.3
%% 
%% This file is automatically generated. DO NOT EDIT IT.
%%
%%------------------------------------------------------------

-module('CosTime_TimeService').
-ic_compiled("4_3_3").


%% Interface functions
-export([universal_time/1, universal_time/2, secure_universal_time/1]).
-export([secure_universal_time/2, new_universal_time/4, new_universal_time/5]).
-export([uto_from_utc/2, uto_from_utc/3, new_interval/3]).
-export([new_interval/4]).

%% Type identification function
-export([typeID/0]).

%% Used to start server
-export([oe_create/0, oe_create_link/0, oe_create/1]).
-export([oe_create_link/1, oe_create/2, oe_create_link/2]).

%% TypeCode Functions and inheritance
-export([oe_tc/1, oe_is_a/1, oe_get_interface/0]).

%% gen server export stuff
-behaviour(gen_server).
-export([init/1, terminate/2, handle_call/3]).
-export([handle_cast/2, handle_info/2, code_change/3]).

-include_lib("orber/include/corba.hrl").


%%------------------------------------------------------------
%%
%% Object interface functions.
%%
%%------------------------------------------------------------



%%%% Operation: universal_time
%% 
%%   Returns: RetVal
%%   Raises:  CosTime::TimeUnavailable
%%
universal_time(OE_THIS) ->
    corba:call(OE_THIS, universal_time, [], ?MODULE).

universal_time(OE_THIS, OE_Options) ->
    corba:call(OE_THIS, universal_time, [], ?MODULE, OE_Options).

%%%% Operation: secure_universal_time
%% 
%%   Returns: RetVal
%%   Raises:  CosTime::TimeUnavailable
%%
secure_universal_time(OE_THIS) ->
    corba:call(OE_THIS, secure_universal_time, [], ?MODULE).

secure_universal_time(OE_THIS, OE_Options) ->
    corba:call(OE_THIS, secure_universal_time, [], ?MODULE, OE_Options).

%%%% Operation: new_universal_time
%% 
%%   Returns: RetVal
%%
new_universal_time(OE_THIS, Time, Inaccuracy, Tdf) ->
    corba:call(OE_THIS, new_universal_time, [Time, Inaccuracy, Tdf], ?MODULE).

new_universal_time(OE_THIS, OE_Options, Time, Inaccuracy, Tdf) ->
    corba:call(OE_THIS, new_universal_time, [Time, Inaccuracy, Tdf], ?MODULE, OE_Options).

%%%% Operation: uto_from_utc
%% 
%%   Returns: RetVal
%%
uto_from_utc(OE_THIS, Utc) ->
    corba:call(OE_THIS, uto_from_utc, [Utc], ?MODULE).

uto_from_utc(OE_THIS, OE_Options, Utc) ->
    corba:call(OE_THIS, uto_from_utc, [Utc], ?MODULE, OE_Options).

%%%% Operation: new_interval
%% 
%%   Returns: RetVal
%%
new_interval(OE_THIS, Lower, Upper) ->
    corba:call(OE_THIS, new_interval, [Lower, Upper], ?MODULE).

new_interval(OE_THIS, OE_Options, Lower, Upper) ->
    corba:call(OE_THIS, new_interval, [Lower, Upper], ?MODULE, OE_Options).

%%------------------------------------------------------------
%%
%% Inherited Interfaces
%%
%%------------------------------------------------------------
oe_is_a("IDL:omg.org/CosTime/TimeService:1.0") -> true;
oe_is_a(_) -> false.

%%------------------------------------------------------------
%%
%% Interface TypeCode
%%
%%------------------------------------------------------------
oe_tc(universal_time) -> 
	{{tk_objref,"IDL:omg.org/CosTime/UTO:1.0","UTO"},[],[]};
oe_tc(secure_universal_time) -> 
	{{tk_objref,"IDL:omg.org/CosTime/UTO:1.0","UTO"},[],[]};
oe_tc(new_universal_time) -> 
	{{tk_objref,"IDL:omg.org/CosTime/UTO:1.0","UTO"},
         [tk_ulonglong,tk_ulonglong,tk_short],
         []};
oe_tc(uto_from_utc) -> 
	{{tk_objref,"IDL:omg.org/CosTime/UTO:1.0","UTO"},
         [{tk_struct,"IDL:omg.org/TimeBase/UtcT:1.0","UtcT",
                     [{"time",tk_ulonglong},
                      {"inacclo",tk_ulong},
                      {"inacchi",tk_ushort},
                      {"tdf",tk_short}]}],
         []};
oe_tc(new_interval) -> 
	{{tk_objref,"IDL:omg.org/CosTime/TIO:1.0","TIO"},
         [tk_ulonglong,tk_ulonglong],
         []};
oe_tc(_) -> undefined.

oe_get_interface() -> 
	[{"new_interval", oe_tc(new_interval)},
	{"uto_from_utc", oe_tc(uto_from_utc)},
	{"new_universal_time", oe_tc(new_universal_time)},
	{"secure_universal_time", oe_tc(secure_universal_time)},
	{"universal_time", oe_tc(universal_time)}].




%%------------------------------------------------------------
%%
%% Object server implementation.
%%
%%------------------------------------------------------------


%%------------------------------------------------------------
%%
%% Function for fetching the interface type ID.
%%
%%------------------------------------------------------------

typeID() ->
    "IDL:omg.org/CosTime/TimeService:1.0".


%%------------------------------------------------------------
%%
%% Object creation functions.
%%
%%------------------------------------------------------------

oe_create() ->
    corba:create(?MODULE, "IDL:omg.org/CosTime/TimeService:1.0").

oe_create_link() ->
    corba:create_link(?MODULE, "IDL:omg.org/CosTime/TimeService:1.0").

oe_create(Env) ->
    corba:create(?MODULE, "IDL:omg.org/CosTime/TimeService:1.0", Env).

oe_create_link(Env) ->
    corba:create_link(?MODULE, "IDL:omg.org/CosTime/TimeService:1.0", Env).

oe_create(Env, RegName) ->
    corba:create(?MODULE, "IDL:omg.org/CosTime/TimeService:1.0", Env, RegName).

oe_create_link(Env, RegName) ->
    corba:create_link(?MODULE, "IDL:omg.org/CosTime/TimeService:1.0", Env, RegName).

%%------------------------------------------------------------
%%
%% Init & terminate functions.
%%
%%------------------------------------------------------------

init(Env) ->
%% Call to implementation init
    corba:handle_init('CosTime_TimeService_impl', Env).

terminate(Reason, State) ->
    corba:handle_terminate('CosTime_TimeService_impl', Reason, State).


%%%% Operation: universal_time
%% 
%%   Returns: RetVal
%%   Raises:  CosTime::TimeUnavailable
%%
handle_call({OE_THIS, OE_Context, universal_time, []}, _, OE_State) ->
  corba:handle_call('CosTime_TimeService_impl', universal_time, [], OE_State, OE_Context, OE_THIS, false);

%%%% Operation: secure_universal_time
%% 
%%   Returns: RetVal
%%   Raises:  CosTime::TimeUnavailable
%%
handle_call({OE_THIS, OE_Context, secure_universal_time, []}, _, OE_State) ->
  corba:handle_call('CosTime_TimeService_impl', secure_universal_time, [], OE_State, OE_Context, OE_THIS, false);

%%%% Operation: new_universal_time
%% 
%%   Returns: RetVal
%%
handle_call({OE_THIS, OE_Context, new_universal_time, [Time, Inaccuracy, Tdf]}, _, OE_State) ->
  corba:handle_call('CosTime_TimeService_impl', new_universal_time, [Time, Inaccuracy, Tdf], OE_State, OE_Context, OE_THIS, false);

%%%% Operation: uto_from_utc
%% 
%%   Returns: RetVal
%%
handle_call({OE_THIS, OE_Context, uto_from_utc, [Utc]}, _, OE_State) ->
  corba:handle_call('CosTime_TimeService_impl', uto_from_utc, [Utc], OE_State, OE_Context, OE_THIS, false);

%%%% Operation: new_interval
%% 
%%   Returns: RetVal
%%
handle_call({OE_THIS, OE_Context, new_interval, [Lower, Upper]}, _, OE_State) ->
  corba:handle_call('CosTime_TimeService_impl', new_interval, [Lower, Upper], OE_State, OE_Context, OE_THIS, false);



%%%% Standard gen_server call handle
%%
handle_call(stop, _, State) ->
    {stop, normal, ok, State};

handle_call(_, _, State) ->
    {reply, catch corba:raise(#'BAD_OPERATION'{minor=1163001857, completion_status='COMPLETED_NO'}), State}.


%%%% Standard gen_server cast handle
%%
handle_cast(stop, State) ->
    {stop, normal, State};

handle_cast(_, State) ->
    {noreply, State}.


%%%% Standard gen_server handles
%%
handle_info(Info, State) ->
    corba:handle_info('CosTime_TimeService_impl', Info, State).


code_change(OldVsn, State, Extra) ->
    corba:handle_code_change('CosTime_TimeService_impl', OldVsn, State, Extra).

