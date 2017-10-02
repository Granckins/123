%%------------------------------------------------------------
%%
%% Implementation stub file
%% 
%% Target: oe_cos_naming
%% Source: /net/isildur/ldisk/daily_build/r16b02_prebuild_opu_o.2013-09-16_20/otp_src_R16B02/lib/orber/COSS/CosNaming/cos_naming.idl
%% IC vsn: 4.3.3
%% 
%% This file is automatically generated. DO NOT EDIT IT.
%%
%%------------------------------------------------------------

-module(oe_cos_naming).
-ic_compiled("4_3_3").


-include_lib("orber/include/ifr_types.hrl").

%% Interface functions

-export([oe_register/0, oe_unregister/0, oe_get_module/5]).
-export([oe_dependency/0]).



oe_register() ->
    OE_IFR = orber_ifr:find_repository(),

    register_tests(OE_IFR),

    _OE_1 = oe_get_top_module(OE_IFR, "IDL:omg.org/CosNaming:1.0", "CosNaming", "1.0"),

    orber_ifr:'ModuleDef_create_alias'(_OE_1, "IDL:omg.org/CosNaming/Istring:1.0", "Istring", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_string,0})),

    orber_ifr:'ModuleDef_create_struct'(_OE_1, "IDL:omg.org/CosNaming/NameComponent:1.0", "NameComponent", "1.0", [#structmember{name="id", type={tk_string,0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_string,0})}, #structmember{name="kind", type={tk_string,0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_string,0})}]),

    orber_ifr:'ModuleDef_create_alias'(_OE_1, "IDL:omg.org/CosNaming/Name:1.0", "Name", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0})),

    orber_ifr:'ModuleDef_create_enum'(_OE_1, "IDL:omg.org/CosNaming/BindingType:1.0", "BindingType", "1.0", ["nobject","ncontext"]),

    orber_ifr:'ModuleDef_create_struct'(_OE_1, "IDL:omg.org/CosNaming/Binding:1.0", "Binding", "1.0", [#structmember{name="binding_name", type={tk_sequence,
                                         {tk_struct,
                                          "IDL:omg.org/CosNaming/NameComponent:1.0",
                                          "NameComponent",
                                          [{"id",{tk_string,0}},
                                           {"kind",{tk_string,0}}]},
                                         0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0})}, #structmember{name="binding_type", type={tk_enum,
                                         "IDL:omg.org/CosNaming/BindingType:1.0",
                                         "BindingType",
                                         ["nobject","ncontext"]}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_enum,
                                               "IDL:omg.org/CosNaming/BindingType:1.0",
                                               "BindingType",
                                               ["nobject","ncontext"]})}]),

    orber_ifr:'ModuleDef_create_alias'(_OE_1, "IDL:omg.org/CosNaming/BindingList:1.0", "BindingList", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/Binding:1.0",
                                                "Binding",
                                                [{"binding_name",
                                                  {tk_sequence,
                                                   {tk_struct,
                                                    "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                    "NameComponent",
                                                    [{"id",{tk_string,0}},
                                                     {"kind",{tk_string,0}}]},
                                                   0}},
                                                 {"binding_type",
                                                  {tk_enum,
                                                   "IDL:omg.org/CosNaming/BindingType:1.0",
                                                   "BindingType",
                                                   ["nobject","ncontext"]}}]},
                                               0})),

    _OE_2 = orber_ifr:'ModuleDef_create_interface'(_OE_1, "IDL:omg.org/CosNaming/NamingContext:1.0", "NamingContext", "1.0", []),

    orber_ifr:'InterfaceDef_create_enum'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/NotFoundReason:1.0", "NotFoundReason", "1.0", ["missing_node","not_context","not_object"]),

    orber_ifr:'InterfaceDef_create_exception'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/NotFound:1.0", "NotFound", "1.0", [#structmember{name="why", type={tk_enum,
                                   "IDL:omg.org/CosNaming/NamingContext/NotFoundReason:1.0",
                                   "NotFoundReason",
                                   ["missing_node","not_context",
                                    "not_object"]}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_enum,
                                               "IDL:omg.org/CosNaming/NamingContext/NotFoundReason:1.0",
                                               "NotFoundReason",
                                               ["missing_node","not_context",
                                                "not_object"]})}, #structmember{name="rest_of_name", type={tk_sequence,
                                         {tk_struct,
                                          "IDL:omg.org/CosNaming/NameComponent:1.0",
                                          "NameComponent",
                                          [{"id",{tk_string,0}},
                                           {"kind",{tk_string,0}}]},
                                         0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0})}]),

    orber_ifr:'InterfaceDef_create_exception'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/CannotProceed:1.0", "CannotProceed", "1.0", [#structmember{name="cxt", type={tk_objref,
                                   "IDL:omg.org/CosNaming/NamingContext:1.0",
                                   "NamingContext"}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_objref,
                                               "IDL:omg.org/CosNaming/NamingContext:1.0",
                                               "NamingContext"})}, #structmember{name="rest_of_name", type={tk_sequence,
                                         {tk_struct,
                                          "IDL:omg.org/CosNaming/NameComponent:1.0",
                                          "NameComponent",
                                          [{"id",{tk_string,0}},
                                           {"kind",{tk_string,0}}]},
                                         0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0})}]),

    orber_ifr:'InterfaceDef_create_exception'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/InvalidName:1.0", "InvalidName", "1.0", []),

    orber_ifr:'InterfaceDef_create_exception'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/AlreadyBound:1.0", "AlreadyBound", "1.0", []),

    orber_ifr:'InterfaceDef_create_exception'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/NotEmpty:1.0", "NotEmpty", "1.0", []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/bind:1.0", "bind", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, tk_void), 'OP_NORMAL', [#parameterdescription{name="obj", type={tk_objref,[],"Object"}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_objref,[],"Object"}), mode='PARAM_IN'}
, #parameterdescription{name="n", type={tk_sequence,
                                      {tk_struct,
                                       "IDL:omg.org/CosNaming/NameComponent:1.0",
                                       "NameComponent",
                                       [{"id",{tk_string,0}},
                                        {"kind",{tk_string,0}}]},
                                      0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0}), mode='PARAM_IN'}
], [orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/AlreadyBound:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/InvalidName:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/CannotProceed:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/NotFound:1.0")], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/rebind:1.0", "rebind", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, tk_void), 'OP_NORMAL', [#parameterdescription{name="obj", type={tk_objref,[],"Object"}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_objref,[],"Object"}), mode='PARAM_IN'}
, #parameterdescription{name="n", type={tk_sequence,
                                      {tk_struct,
                                       "IDL:omg.org/CosNaming/NameComponent:1.0",
                                       "NameComponent",
                                       [{"id",{tk_string,0}},
                                        {"kind",{tk_string,0}}]},
                                      0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0}), mode='PARAM_IN'}
], [orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/InvalidName:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/CannotProceed:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/NotFound:1.0")], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/bind_context:1.0", "bind_context", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, tk_void), 'OP_NORMAL', [#parameterdescription{name="nc", type={tk_objref,
                                       "IDL:omg.org/CosNaming/NamingContext:1.0",
                                       "NamingContext"}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_objref,
                                               "IDL:omg.org/CosNaming/NamingContext:1.0",
                                               "NamingContext"}), mode='PARAM_IN'}
, #parameterdescription{name="n", type={tk_sequence,
                                      {tk_struct,
                                       "IDL:omg.org/CosNaming/NameComponent:1.0",
                                       "NameComponent",
                                       [{"id",{tk_string,0}},
                                        {"kind",{tk_string,0}}]},
                                      0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0}), mode='PARAM_IN'}
], [orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/AlreadyBound:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/InvalidName:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/CannotProceed:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/NotFound:1.0")], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/rebind_context:1.0", "rebind_context", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, tk_void), 'OP_NORMAL', [#parameterdescription{name="nc", type={tk_objref,
                                       "IDL:omg.org/CosNaming/NamingContext:1.0",
                                       "NamingContext"}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_objref,
                                               "IDL:omg.org/CosNaming/NamingContext:1.0",
                                               "NamingContext"}), mode='PARAM_IN'}
, #parameterdescription{name="n", type={tk_sequence,
                                      {tk_struct,
                                       "IDL:omg.org/CosNaming/NameComponent:1.0",
                                       "NameComponent",
                                       [{"id",{tk_string,0}},
                                        {"kind",{tk_string,0}}]},
                                      0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0}), mode='PARAM_IN'}
], [orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/InvalidName:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/CannotProceed:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/NotFound:1.0")], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/resolve:1.0", "resolve", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_objref,[],"Object"}), 'OP_NORMAL', [#parameterdescription{name="n", type={tk_sequence,
                                      {tk_struct,
                                       "IDL:omg.org/CosNaming/NameComponent:1.0",
                                       "NameComponent",
                                       [{"id",{tk_string,0}},
                                        {"kind",{tk_string,0}}]},
                                      0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0}), mode='PARAM_IN'}
], [orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/InvalidName:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/CannotProceed:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/NotFound:1.0")], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/unbind:1.0", "unbind", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, tk_void), 'OP_NORMAL', [#parameterdescription{name="n", type={tk_sequence,
                                      {tk_struct,
                                       "IDL:omg.org/CosNaming/NameComponent:1.0",
                                       "NameComponent",
                                       [{"id",{tk_string,0}},
                                        {"kind",{tk_string,0}}]},
                                      0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0}), mode='PARAM_IN'}
], [orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/InvalidName:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/CannotProceed:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/NotFound:1.0")], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/new_context:1.0", "new_context", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_objref,
                                               "IDL:omg.org/CosNaming/NamingContext:1.0",
                                               "NamingContext"}), 'OP_NORMAL', [], [], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/bind_new_context:1.0", "bind_new_context", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_objref,
                                               "IDL:omg.org/CosNaming/NamingContext:1.0",
                                               "NamingContext"}), 'OP_NORMAL', [#parameterdescription{name="n", type={tk_sequence,
                                      {tk_struct,
                                       "IDL:omg.org/CosNaming/NameComponent:1.0",
                                       "NameComponent",
                                       [{"id",{tk_string,0}},
                                        {"kind",{tk_string,0}}]},
                                      0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                "NameComponent",
                                                [{"id",{tk_string,0}},
                                                 {"kind",{tk_string,0}}]},
                                               0}), mode='PARAM_IN'}
], [orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/InvalidName:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/CannotProceed:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/AlreadyBound:1.0"), orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/NotFound:1.0")], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/destroy:1.0", "destroy", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, tk_void), 'OP_NORMAL', [], [orber_ifr:lookup_id(OE_IFR,"IDL:omg.org/CosNaming/NamingContext/NotEmpty:1.0")], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_2, "IDL:omg.org/CosNaming/NamingContext/list:1.0", "list", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, tk_void), 'OP_NORMAL', [#parameterdescription{name="bi", type={tk_objref,
                                       "IDL:omg.org/CosNaming/BindingIterator:1.0",
                                       "BindingIterator"}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_objref,
                                               "IDL:omg.org/CosNaming/BindingIterator:1.0",
                                               "BindingIterator"}), mode='PARAM_OUT'}
, #parameterdescription{name="bl", type={tk_sequence,
                                       {tk_struct,
                                        "IDL:omg.org/CosNaming/Binding:1.0",
                                        "Binding",
                                        [{"binding_name",
                                          {tk_sequence,
                                           {tk_struct,
                                            "IDL:omg.org/CosNaming/NameComponent:1.0",
                                            "NameComponent",
                                            [{"id",{tk_string,0}},
                                             {"kind",{tk_string,0}}]},
                                           0}},
                                         {"binding_type",
                                          {tk_enum,
                                           "IDL:omg.org/CosNaming/BindingType:1.0",
                                           "BindingType",
                                           ["nobject","ncontext"]}}]},
                                       0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/Binding:1.0",
                                                "Binding",
                                                [{"binding_name",
                                                  {tk_sequence,
                                                   {tk_struct,
                                                    "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                    "NameComponent",
                                                    [{"id",{tk_string,0}},
                                                     {"kind",{tk_string,0}}]},
                                                   0}},
                                                 {"binding_type",
                                                  {tk_enum,
                                                   "IDL:omg.org/CosNaming/BindingType:1.0",
                                                   "BindingType",
                                                   ["nobject","ncontext"]}}]},
                                               0}), mode='PARAM_OUT'}
, #parameterdescription{name="how_many", type=tk_ulong, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, tk_ulong), mode='PARAM_IN'}
], [], []),

    _OE_3 = orber_ifr:'ModuleDef_create_interface'(_OE_1, "IDL:omg.org/CosNaming/BindingIterator:1.0", "BindingIterator", "1.0", []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_3, "IDL:omg.org/CosNaming/BindingIterator/next_one:1.0", "next_one", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, tk_boolean), 'OP_NORMAL', [#parameterdescription{name="b", type={tk_struct,
                                      "IDL:omg.org/CosNaming/Binding:1.0",
                                      "Binding",
                                      [{"binding_name",
                                        {tk_sequence,
                                         {tk_struct,
                                          "IDL:omg.org/CosNaming/NameComponent:1.0",
                                          "NameComponent",
                                          [{"id",{tk_string,0}},
                                           {"kind",{tk_string,0}}]},
                                         0}},
                                       {"binding_type",
                                        {tk_enum,
                                         "IDL:omg.org/CosNaming/BindingType:1.0",
                                         "BindingType",
                                         ["nobject","ncontext"]}}]}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_struct,
                                               "IDL:omg.org/CosNaming/Binding:1.0",
                                               "Binding",
                                               [{"binding_name",
                                                 {tk_sequence,
                                                  {tk_struct,
                                                   "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                   "NameComponent",
                                                   [{"id",{tk_string,0}},
                                                    {"kind",{tk_string,0}}]},
                                                  0}},
                                                {"binding_type",
                                                 {tk_enum,
                                                  "IDL:omg.org/CosNaming/BindingType:1.0",
                                                  "BindingType",
                                                  ["nobject","ncontext"]}}]}), mode='PARAM_OUT'}
], [], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_3, "IDL:omg.org/CosNaming/BindingIterator/next_n:1.0", "next_n", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, tk_boolean), 'OP_NORMAL', [#parameterdescription{name="bl", type={tk_sequence,
                                       {tk_struct,
                                        "IDL:omg.org/CosNaming/Binding:1.0",
                                        "Binding",
                                        [{"binding_name",
                                          {tk_sequence,
                                           {tk_struct,
                                            "IDL:omg.org/CosNaming/NameComponent:1.0",
                                            "NameComponent",
                                            [{"id",{tk_string,0}},
                                             {"kind",{tk_string,0}}]},
                                           0}},
                                         {"binding_type",
                                          {tk_enum,
                                           "IDL:omg.org/CosNaming/BindingType:1.0",
                                           "BindingType",
                                           ["nobject","ncontext"]}}]},
                                       0}, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, {tk_sequence,
                                               {tk_struct,
                                                "IDL:omg.org/CosNaming/Binding:1.0",
                                                "Binding",
                                                [{"binding_name",
                                                  {tk_sequence,
                                                   {tk_struct,
                                                    "IDL:omg.org/CosNaming/NameComponent:1.0",
                                                    "NameComponent",
                                                    [{"id",{tk_string,0}},
                                                     {"kind",{tk_string,0}}]},
                                                   0}},
                                                 {"binding_type",
                                                  {tk_enum,
                                                   "IDL:omg.org/CosNaming/BindingType:1.0",
                                                   "BindingType",
                                                   ["nobject","ncontext"]}}]},
                                               0}), mode='PARAM_OUT'}
, #parameterdescription{name="how_many", type=tk_ulong, type_def=orber_ifr:'Repository_create_idltype'(OE_IFR, tk_ulong), mode='PARAM_IN'}
], [], []),

    orber_ifr:'InterfaceDef_create_operation'(_OE_3, "IDL:omg.org/CosNaming/BindingIterator/destroy:1.0", "destroy", "1.0", orber_ifr:'Repository_create_idltype'(OE_IFR, tk_void), 'OP_NORMAL', [], [], []),

    ok.


%% General IFR registration checks.
register_tests(OE_IFR)->
  re_register_test(OE_IFR),
  include_reg_test(OE_IFR).


%% IFR type Re-registration checks.
re_register_test(OE_IFR)->
  case orber_ifr:'Repository_lookup_id'(OE_IFR,"IDL:omg.org/CosNaming/Istring:1.0") of
    []  ->
      true;
    _ ->
      exit({allready_registered,"IDL:omg.org/CosNaming/Istring:1.0"})
 end.


%% No included idl-files detected.
include_reg_test(_OE_IFR) -> true.


%% Fetch top module reference, register if unregistered.
oe_get_top_module(OE_IFR, ID, Name, Version) ->
  case orber_ifr:'Repository_lookup_id'(OE_IFR, ID) of
    [] ->
      orber_ifr:'Repository_create_module'(OE_IFR, ID, Name, Version);
    Mod  ->
      Mod
   end.

%% Fetch module reference, register if unregistered.
oe_get_module(OE_IFR, OE_Parent, ID, Name, Version) ->
  case orber_ifr:'Repository_lookup_id'(OE_IFR, ID) of
    [] ->
      orber_ifr:'ModuleDef_create_module'(OE_Parent, ID, Name, Version);
    Mod  ->
      Mod
   end.



oe_unregister() ->
    OE_IFR = orber_ifr:find_repository(),

    oe_destroy(OE_IFR, "IDL:omg.org/CosNaming/BindingIterator:1.0"),
    oe_destroy(OE_IFR, "IDL:omg.org/CosNaming/NamingContext:1.0"),
    oe_destroy(OE_IFR, "IDL:omg.org/CosNaming/BindingList:1.0"),
    oe_destroy(OE_IFR, "IDL:omg.org/CosNaming/Binding:1.0"),
    oe_destroy(OE_IFR, "IDL:omg.org/CosNaming/BindingType:1.0"),
    oe_destroy(OE_IFR, "IDL:omg.org/CosNaming/Name:1.0"),
    oe_destroy(OE_IFR, "IDL:omg.org/CosNaming/NameComponent:1.0"),
    oe_destroy(OE_IFR, "IDL:omg.org/CosNaming/Istring:1.0"),
    oe_destroy_if_empty(OE_IFR, "IDL:omg.org/CosNaming:1.0"),
    ok.


oe_destroy_if_empty(OE_IFR,IFR_ID) ->
    case orber_ifr:'Repository_lookup_id'(OE_IFR, IFR_ID) of
	[] ->
	    ok;
	Ref ->
	    case orber_ifr:contents(Ref, 'dk_All', 'true') of
		[] ->
		    orber_ifr:destroy(Ref),
		    ok;
		_ ->
		    ok
	    end
    end.

oe_destroy(OE_IFR,IFR_ID) ->
    case orber_ifr:'Repository_lookup_id'(OE_IFR, IFR_ID) of
	[] ->
	    ok;
	Ref ->
	    orber_ifr:destroy(Ref),
	    ok
    end.



%% Idl file dependency list function
oe_dependency() ->

    [].
