%%------------------------------------------------------------
%%
%% Implementation stub file
%% 
%% Target: CosFileTransfer_TransferException
%% Source: /net/isildur/ldisk/daily_build/r16b02_prebuild_opu_o.2013-09-16_20/otp_src_R16B02/lib/cosFileTransfer/src/CosFileTransfer.idl
%% IC vsn: 4.3.3
%% 
%% This file is automatically generated. DO NOT EDIT IT.
%%
%%------------------------------------------------------------

-module('CosFileTransfer_TransferException').
-ic_compiled("4_3_3").


-include("CosFileTransfer.hrl").

-export([tc/0,id/0,name/0]).



%% returns type code
tc() -> {tk_except,"IDL:omg.org/CosFileTransfer/TransferException:1.0",
                   "TransferException",
                   [{"reason",{tk_string,0}}]}.

%% returns id
id() -> "IDL:omg.org/CosFileTransfer/TransferException:1.0".

%% returns name
name() -> "CosFileTransfer_TransferException".



