{application, couch, [
    {description, "Apache CouchDB"},
    {vsn, "1.6.1"},
    {modules, [couch,couch_app,couch_auth_cache,couch_btree,couch_changes,couch_compaction_daemon,couch_compress,couch_config,couch_config_writer,couch_db,couch_db_updater,couch_db_update_notifier,couch_db_update_notifier_sup,couch_doc,couch_drv,couch_ejson_compare,couch_event_sup,couch_external_manager,couch_external_server,couch_file,couch_httpd,couch_httpd_auth,couch_httpd_cors,couch_httpd_db,couch_httpd_external,couch_httpd_misc_handlers,couch_httpd_oauth,couch_httpd_proxy,couch_httpd_rewrite,couch_httpd_stats_handlers,couch_httpd_vhost,couch_key_tree,couch_log,couch_native_process,couch_os_daemons,couch_os_process,couch_passwords,couch_primary_sup,couch_query_servers,couch_ref_counter,couch_secondary_sup,couch_server,couch_server_sup,couch_stats_aggregator,couch_stats_collector,couch_stream,couch_task_status,couch_users_db,couch_util,couch_uuids,couch_work_queue,json_stream_parse]},
    {registered, [
        couch_config,
        couch_db_update,
        couch_db_update_notifier_sup,
        couch_external_manager,
        couch_httpd,
        couch_log,
        couch_primary_services,
        couch_query_servers,
        couch_secondary_services,
        couch_server,
        couch_server_sup,
        couch_stats_aggregator,
        couch_stats_collector,
        couch_task_status
    ]},
    {mod, {couch_app, [
        "../etc/couchdb/default.ini",
        "../etc/couchdb/local.ini"
    ]}},
    {applications, [kernel, stdlib]},
    {included_applications, [crypto, sasl, inets, oauth, ibrowse, mochiweb, os_mon]}
]}.
