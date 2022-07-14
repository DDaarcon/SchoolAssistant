import React from "react";
import LoaderSize from "./enums/loader-size";
import LoaderType from "./enums/loader-type";
import Loader from "./loader";
import './page-blocking-loader.css';

type PageBlockingLoaderProps = {
    className?: string;
    enable?: boolean;
}

const PageBlockingLoader = React.forwardRef<Loader, PageBlockingLoaderProps>((props: PageBlockingLoaderProps, ref: React.ForwardedRef<Loader>) => {
    const className = "page-blocking-loader " + (props.className ?? "");

    return (
        <Loader
            size={LoaderSize.Large}
            type={LoaderType.BlockPage}
            className={className}
            enable={props.enable}
            ref={ref}
        />
    )
});
export default PageBlockingLoader;