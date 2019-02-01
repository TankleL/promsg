
			template<>
			struct as<$(PARAM_NAME)> {
				$(PARAM_NAME) operator()(msgpack::object const& o) const {
					if (o.type != msgpack::type::ARRAY) throw msgpack::type_error();
					$(PARAM_NAME) obj($(CONSTRUCT_PARAM));
					$(DECODE_VALUE)
					return obj;
				}
			};
